using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services;
using System.Configuration;
using System.IO;
using System.Speech.Synthesis;
using System.Speech.AudioFormat;
using Inqwise.AdsCaptcha.API.App_Code;
using Inqwise.AdsCaptcha.DAL;

namespace Inqwise.AdsCaptcha.API.Slider
{
    /// <summary>
    /// Summary description for $CodeFileclassname$
    /// </summary>
    /*
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Speak : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        static Random r = new Random((int)(DateTime.Now.Ticks % 99999));
        public void ProcessRequest(HttpContext context)
        {

            try
            {
                string audioDir = ConfigurationManager.AppSettings["AudioDirectory"];

                string reqid = context.Request.QueryString["cid"];
                int value = (string.IsNullOrEmpty(context.Request.QueryString["val"])) ? 0 : Convert.ToInt32(context.Request.QueryString["val"]);

                if (reqid.Trim() != string.Empty)
                {
                    int challenge = -1;
                    CacheImagesManager cachemanager = new CacheImagesManager();
                    int imageid = ImagesDAL.GetInstance().GetImageIDByRequest(reqid, ref challenge);

                    SliderCorrection correction = SliderCorrection.Correct;

                    if (value == 0)
                    {
                        if (challenge < 3) correction = SliderCorrection.MoveSlowToRight;
                        else correction = SliderCorrection.MoveToRight;
                    }
                    else
                    {
                        if (Math.Abs(challenge - value) < 3)
                        {
                            correction = SliderCorrection.Correct;
                        }
                        else
                        {
                            if (Math.Abs(challenge - value) < 8)
                            {
                                if (challenge > value) correction = SliderCorrection.MoveSlowToRight;
                                else correction = SliderCorrection.MoveSlowToLeft;
                            }
                            else
                            {
                                if (challenge > value) correction = SliderCorrection.MoveToRight;
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

                            new Speach(textToSpeech, fileSpeech).CreateFile();
                        }


                        //HttpContext.Current.Response.Clear();
                        HttpContext.Current.Response.ContentType = "audio/wav";

                        var waveStream = new FileStream(fileSpeech, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                        byte[] bytes = new byte[1024 * 100];//new byte[(int)waveStream.Length];//new byte[1024 * 100];
                        
                        waveStream.Read(bytes, 0, (int)waveStream.Length);

                        for (int j = 0; j < bytes.Length; j++)
                        {
                            int brand = r.Next(99999) % 215;
                            if (brand == 0)
                                bytes[j] = (byte)(r.Next(99999) % 255);
                        }


                        context.Response.OutputStream.Write(bytes, 0, bytes.Length);
                        waveStream.Close();
                    }
                    else
                    {
                        HttpContext.Current.Response.ContentType = "audio/wav";
                        byte[] bytes = new byte[1024 * 100];
                        context.Response.OutputStream.Write(bytes, 0, bytes.Length);
                    }
                }
            }
            catch (Exception exc)
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write(exc.ToString());
            }
            context.Response.End();



        }

        private class Speach
        {
            public string TextToSpeech { get; private set; }
            public string FileSpeech { get; private set; }

            public Speach(string textToSpeech, string fileSpeech)
            {
                TextToSpeech = textToSpeech;
                FileSpeech = fileSpeech;
            }

            public void CreateFile()
            {
                Task t = Task.Run(new Action(Process));
                t.Wait();
            }

            private void Process()
            {
                using (var synthesizer = new SpeechSynthesizer())
                {
                    //var waveStream = new MemoryStream();

                    var pbuilder = new PromptBuilder();
                    var pStyle = new PromptStyle();

                    pStyle.Emphasis = PromptEmphasis.None;
                    pStyle.Rate = PromptRate.Medium;
                    pStyle.Volume = PromptVolume.ExtraLoud;

                    pbuilder.StartStyle(pStyle);
                    pbuilder.StartParagraph();
                    pbuilder.StartVoice(VoiceGender.Female, VoiceAge.Adult, 2);
                    pbuilder.StartSentence();
                    pbuilder.AppendText(TextToSpeech);
                    pbuilder.EndSentence();
                    pbuilder.EndVoice();
                    pbuilder.EndParagraph();
                    pbuilder.EndStyle();


                    synthesizer.SetOutputToWaveFile(FileSpeech, new SpeechAudioFormatInfo(8000, AudioBitsPerSample.Eight, AudioChannel.Mono));
                    //synthesizer.SetOutputToAudioStream(ms, new SpeechAudioFormatInfo(8000, AudioBitsPerSample.Eight, AudioChannel.Mono));
                    //synthesizer.SetOutputToWaveStream(ms);
                    synthesizer.Speak(pbuilder);
                    synthesizer.SetOutputToNull();
                }
            }
        }

        public enum SliderCorrection
        {
            MoveToRight = 0,
            MoveSlowToRight,
            Correct,
            MoveSlowToLeft,
            MoveToLeft
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

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

            using (AdsCaptcha_ImagesEntities ent = new AdsCaptcha_ImagesEntities())
            {
                var blocked = ent.T_REQUESTS_SPEECH.Where(s => s.IsBlocked && s.SessionID == sessionID).ToList();

                if (blocked.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                    var currentSpeech = ent.T_REQUESTS_SPEECH.Where(s => s.RequestID == requestID).FirstOrDefault();
                    if (currentSpeech != null)
                    {
                        if (sessionID != currentSpeech.SessionID)
                        {
                            var speech = new T_REQUESTS_SPEECH();
                            speech.SessionID = sessionID;
                            speech.RequestID = requestID;
                            speech.InsertDate = DateTime.Now;
                            speech.IsBlocked = true;

                            ent.AddToT_REQUESTS_SPEECH(speech);

                            return true;
                        }
                        else
                        {
                            var asks = ent.T_REQUESTS_SPEECH_ASK.Where(s=>s.T_REQUESTS_SPEECH.SpeechID == currentSpeech.SpeechID).ToList();
                            if (asks.Count != 7)
                            {
                                var ask = new T_REQUESTS_SPEECH_ASK();
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
                                var askExp = currentSpeech.T_REQUESTS_SPEECH_ASK.Where(a => a.ExpectedValue > 0).FirstOrDefault();

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
                                        var ask = new T_REQUESTS_SPEECH_ASK();
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
    }
     */
}
     
