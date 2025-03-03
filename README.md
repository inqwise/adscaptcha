# AdsCaptcha

**AdsCaptcha** is an advertisement-based CAPTCHA solution that helps protect websites from spam and bots while allowing site owners to monetize the verification process. Instead of traditional distorted text, AdsCaptcha presents users with ads as the challenge, so every solved CAPTCHA can generate revenue for the website owner.

## Installation

1. **Clone or Download** the repository to your local machine.
2. **Open the Solution** in Visual Studio (AdsCaptcha is a C# ASP.NET project). Make sure you have the required .NET Framework installed (e.g., .NET Framework 4.x).
3. **Restore Dependencies** via NuGet if prompted (Visual Studio should restore any NuGet packages on build).
4. **Configure the Application** – update any necessary settings (for example, database connection strings in the Web.config, email SMTP settings, etc., if applicable) to suit your environment.
5. **Build and Run** the solution. The main web project is **AdsCaptcha.Web** – you can run it via IIS Express or your preferred web server to host the AdsCaptcha service.

## Basic Usage

Once the AdsCaptcha service is running, you can integrate it into your website forms. Typically, you would sign up (or create) a site key and secret pair and then do the following:

### Embedding AdsCaptcha in Forms

```html
<form action="/submit-form" method="POST">
    <div id="adscaptcha-widget"></div>
    <script src="http://YOUR_ADSCAPTCHA_SERVER/api/captcha.js?siteKey=YOUR_SITE_PUBLIC_KEY"></script>
    <noscript>
        <iframe src="http://YOUR_ADSCAPTCHA_SERVER/api/noscript?siteKey=YOUR_SITE_PUBLIC_KEY" width="300" height="200"></iframe><br>
        <input type="text" name="adscaptcha_response" placeholder="Enter CAPTCHA text">
        <input type="hidden" name="adscaptcha_challenge" value="...challenge-id...">
    </noscript>
    <button type="submit">Submit</button>
</form>
```

### Verifying the Response on the Server

```csharp
using AdsCaptcha;  // Assume AdsCaptcha provides a verification helper

string userResponse = Request.Form["adscaptcha_response"];
string challengeId = Request.Form["adscaptcha_challenge"];
bool isHuman = AdsCaptchaValidator.Verify(challengeId, userResponse, YOUR_SITE_SECRET_KEY);
if (isHuman) {
    // Proceed with form processing – CAPTCHA was solved correctly
} else {
    // CAPTCHA failed – handle accordingly (ask user to try again, etc.)
}
```

The exact integration code may vary. AdsCaptcha’s usage is similar to other CAPTCHA services: embed a client-side challenge and verify the user’s answer on the server. Adjust the URLs, function names, and parameters based on the implementation details of this project.

## License

This project is open-source and available under the **MIT License**. See the [LICENSE](LICENSE) file for details.