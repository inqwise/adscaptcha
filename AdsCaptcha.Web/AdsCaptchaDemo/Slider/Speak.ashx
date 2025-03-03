<%@ WebHandler Language="C#" Class="Speak" %>

using System;
using System.Configuration;
using System.Web;
using System.Speech.Synthesis;
using System.Speech.AudioFormat;
using System.IO;
using System.Linq;

public class Speak : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    static Random r = new Random((int)(DateTime.Now.Ticks % 99999));
    public void ProcessRequest (HttpContext context) {

        try
        {
            //string[] audiofiles = { "moveright.wav", 
            //                      "moveslowright.wav", 
            //                      "correct.wav", 
            //                      "moveslowleft.wav", 
            //                      "moveleft.wav" };
            //string[] audiotexts = { "Move the Slider to the right or slide it with the right arrow button and click on the audio button or ALT-+-X again for further instructions", 
            //                    "Move the Slider slowly to the right or slide it with the right arrow button and click on the audio button or ALT-+-X again for further instructions", 
            //                    "The slider is in the correct position", 
            //                    "Move the Slider slowly to the left or slide it with the left arrow button and click on the audio button or ALT-+-X  again for further instructions", 
            //                    "Move the Slider to the left or slide it with the left arrow button and click on the audio button or ALT-+-X  again for further instructions" };
            string audioDir = ConfigurationManager.AppSettings["AudioDirectory"];

            string reqid = context.Request.QueryString["cid"];
            int value = (string.IsNullOrEmpty(context.Request.QueryString["val"])) ? 0 : Convert.ToInt32(context.Request.QueryString["val"]);
            SliderCorrection correction = SliderCorrection.Correct;
            
            if (reqid.Trim() != string.Empty)
            {
                /*
                Request cacherequest = (Request)CacheManager.Instance.GetCachedItem(reqid);

                

                if (value == 0)
                {
                    if (cacherequest.Challenge < 3) correction = SliderCorrection.MoveSlowToRight;
                    else correction = SliderCorrection.MoveToRight;
                }
                else
                {
                    if (Math.Abs(cacherequest.Challenge - value) < 3)
                    {
                        correction = SliderCorrection.Correct;
                    }
                    else
                    {
                        if (Math.Abs(cacherequest.Challenge - value) < 8)
                        {
                            if (cacherequest.Challenge > value) correction = SliderCorrection.MoveSlowToRight;
                            else correction = SliderCorrection.MoveSlowToLeft;
                        }
                        else
                        {
                            if (cacherequest.Challenge > value) correction = SliderCorrection.MoveToRight;
                            else correction = SliderCorrection.MoveToLeft;
                        }
                    }
                }

                string lang = string.Empty;

                AudioDataSet ds = new AudioDataSet();
                ds.ReadXml(context.Server.MapPath("ConfigFiles/AudioVariant" + lang + ".xml"));

                string botText = string.Empty;
                string botFile = string.Empty;
                string fileSpeech = string.Empty;
                string textToSpeech = string.Empty; 

                if (!IsBotCheck(context.Session.SessionID, reqid, value, correction, ref botText, ref botFile))
                {

                    var rows = ds.Tables["Variant"].Select("type=" + ((int)correction + 1).ToString());
                    int currentIndex = 0;

                    if (rows.Length > 1)
                    {
                        r.Next(1000 + (int)(DateTime.Now.Ticks % 999));

                        currentIndex = r.Next(rows.Length);
                    }
                    string file = rows[currentIndex]["File"].ToString();

                    textToSpeech = rows[currentIndex]["Text"].ToString() + ", ";


                    GetInstruction(ds, value, correction, context.Session.SessionID, reqid, ref file, ref textToSpeech);

                    fileSpeech = audioDir + file + ".wav";
                }
                else
                {
                    fileSpeech = audioDir + botFile + ".wav";
                    textToSpeech = botText;
                }
                if (textToSpeech.Trim() != string.Empty)
                {
                    if (!File.Exists(fileSpeech))
                    {




                        System.Speech.AudioFormat.SpeechAudioFormatInfo synthFormat =
                                        new System.Speech.AudioFormat.SpeechAudioFormatInfo(System.Speech.AudioFormat.EncodingFormat.Pcm,
                                            11025, 16, 1, 16000, 2, null);


                        using (SpeechSynthesizer synthesizer = new SpeechSynthesizer())
                        {
                            //var waveStream = new MemoryStream();

                            PromptBuilder pbuilder = new PromptBuilder();
                            PromptStyle pStyle = new PromptStyle();

                            pStyle.Emphasis = PromptEmphasis.None;
                            pStyle.Rate = PromptRate.Medium;
                            pStyle.Volume = PromptVolume.ExtraLoud;

                            pbuilder.StartStyle(pStyle);
                            pbuilder.StartParagraph();
                            pbuilder.StartVoice(VoiceGender.Female, VoiceAge.Adult, 2);
                            pbuilder.StartSentence();
                            pbuilder.AppendText(textToSpeech);
                            pbuilder.EndSentence();
                            pbuilder.EndVoice();
                            pbuilder.EndParagraph();
                            pbuilder.EndStyle();

                            synthesizer.SetOutputToWaveFile(fileSpeech, new SpeechAudioFormatInfo(8000, AudioBitsPerSample.Eight, AudioChannel.Mono));
                            //synthesizer.SetOutputToAudioStream(ms, new SpeechAudioFormatInfo(8000, AudioBitsPerSample.Eight, AudioChannel.Mono));
                            //synthesizer.SetOutputToWaveStream(ms);
                            synthesizer.Speak(pbuilder);
                            synthesizer.SetOutputToNull();

                        }

                        GC.Collect();
                    }


                    //HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.ContentType = "audio/wav";

                    var waveStream = new FileStream(fileSpeech, FileMode.Open, FileAccess.Read);
                    byte[] bytes = new byte[1024 * 100];
                    waveStream.Read(bytes, 0, (int)waveStream.Length);
                    context.Response.OutputStream.Write(bytes, 0, bytes.Length);
                    waveStream.Close();
                }
                else
                {
                    HttpContext.Current.Response.ContentType = "audio/wav";
                    byte[] bytes = new byte[1024 * 100];
                    context.Response.OutputStream.Write(bytes, 0, bytes.Length);
                }
                 */
            }

        }
        catch (Exception exc)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write(exc.ToString());
        }
        context.Response.End();
        
        
        
    }

    public enum SliderCorrection
    {
        MoveToRight = 0,
        MoveSlowToRight,
        Correct,
        MoveSlowToLeft,
        MoveToLeft
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

    /*
    public void GetInstruction(AudioDataSet ds, int value, SliderCorrection currentCorrection, 
                            string sessionID, string requestID, ref string fileAdd, ref string textAdd)
    {
         var instuctions = ds.Tables["Variant"].Select("type=6");
         int currentIndex = 0;
         r.Next(1000 + (int)(DateTime.Now.Ticks % 999));
         if (value == 0)
         {
             currentIndex = r.Next(instuctions.Length);
             fileAdd += "i" + instuctions[currentIndex]["ID"].ToString();
             textAdd += instuctions[currentIndex]["Text"].ToString();
         }
         else
         {
             bool isAdd = r.Next(999) % 3 == 0;
             if (isAdd)
             {
                 if (currentCorrection != SliderCorrection.Correct)
                 {
                     currentIndex = r.Next(instuctions.Length);
                     fileAdd += "i" + instuctions[currentIndex]["ID"].ToString();
                     textAdd += instuctions[currentIndex]["Text"].ToString();
                 }

             }
             else
             {
                 if (currentCorrection == SliderCorrection.Correct)
                 {
                     var submits = ds.Tables["Variant"].Select("type=7");
                     currentIndex = r.Next(submits.Length);
                     fileAdd += "s" + submits[currentIndex]["ID"].ToString();
                     textAdd += submits[currentIndex]["Text"].ToString();
                 }
             }
         }
    }

    public string BotDetectText = "Warning! Your action looks like a bot attack, move the slider to the XXX for YYY positions and click on the audio button or ALT-+-X again for further instructions";
    public bool IsBotCheck(string sessionID, string requestID, int value, 
                        SliderCorrection currentCorrection, ref string botText, ref string botFile)
    {
        
        using (AdsCaptcha_DemoModel.AdsCaptcha_ImagesEntities ent = new AdsCaptcha_DemoModel.AdsCaptcha_ImagesEntities())
        {
            var blocked = ent.T_REQUESTS_SPEECH.Where(s => s.IsBlocked && s.SessionID == sessionID).ToList();

            if (blocked.Count > 0)
            {
                return true;
            }
            else
            {
                var currentSpeech = ent.T_REQUESTS_SPEECH.Where(s => s.RequestID == requestID).FirstOrDefault();
                if (currentSpeech != null)
                {
                    if (sessionID != currentSpeech.SessionID)
                    {
                        var speech = new AdsCaptcha_DemoModel.T_REQUESTS_SPEECH();
                        speech.SessionID = sessionID;
                        speech.RequestID = requestID;
                        speech.InsertDate = DateTime.Now;
                        speech.IsBlocked = true;

                        ent.AddToT_REQUESTS_SPEECH(speech);

                        return true;
                    }
                    else
                    {
                        var asks = currentSpeech.T_REQUESTS_SPEECH_ASK.ToList();
                        if (asks.Count != 7)
                        {
                            var ask = new AdsCaptcha_DemoModel.T_REQUESTS_SPEECH_ASK();
                            ask.Value = value;
                            
                            ask.InsertDate = DateTime.Now;

                            if (asks.Count == 6 && currentCorrection != SliderCorrection.Correct)
                            {
                                int nextAdd = r.Next(9999) % 6 + 3;
                                int expected = 0;
                                string direction = "right";
                                if (currentCorrection == SliderCorrection.MoveToLeft || currentCorrection == SliderCorrection.MoveSlowToLeft)
                                    direction = "left";
                                if (direction == "right")
                                {
                                    expected = nextAdd + value;
                                    if (expected > 29) expected = 29;
                                }
                                else
                                {
                                    expected = value - nextAdd;
                                    if (expected < 0) expected = 0;
                                }


                                botText = BotDetectText.Replace("XXX", direction).Replace("YYY", Math.Abs(expected - value).ToString());
                                botFile = "bot" + direction + Math.Abs(expected - value).ToString();

                                ask.ExpectedValue = expected;
                                
                                currentSpeech.T_REQUESTS_SPEECH_ASK.Add(ask);
                                ent.SaveChanges();
                                return true;
                            }
                            else
                            {
                                ask.ExpectedValue = 0;

                                currentSpeech.T_REQUESTS_SPEECH_ASK.Add(ask);
                                ent.SaveChanges();
                                return false;
                            }
                        }
                        else
                        {
                            var askExp = currentSpeech.T_REQUESTS_SPEECH_ASK.Where(a=>a.ExpectedValue > 0).FirstOrDefault();

                            if (value == askExp.Value)
                            {
                                string direction = "right";
                                if (askExp.ExpectedValue < askExp.Value) direction = "left";
                                botText = BotDetectText.Replace("XXX", direction).Replace("YYY", Math.Abs(askExp.ExpectedValue - value).ToString());
                                botFile = "bot" + direction + Math.Abs(askExp.ExpectedValue - value).ToString();
                                return true;
                            }
                            else
                            {
                                if (Math.Abs(askExp.ExpectedValue - value) > 1)
                                {
                                    currentSpeech.IsBlocked = true;
                                    ent.SaveChanges();
                                    return true;
                                }
                                else
                                {
                                    var ask = new AdsCaptcha_DemoModel.T_REQUESTS_SPEECH_ASK();
                                    ask.Value = value;
                                    ask.ExpectedValue = 0;
                                    ask.InsertDate = DateTime.Now;
                                    currentSpeech.T_REQUESTS_SPEECH_ASK.Add(ask);
                                    ent.SaveChanges();
                                    return false;
                                    
                                }
                            }
                        }
                    }
                }
                else
                {
                    return true;
                }
            }
            
        }


        return true;
    }
     */

}