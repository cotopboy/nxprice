using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Speech.Synthesis;
using System.Globalization;

namespace nxprice_lib.Robot
{
    public class Speaker
    {
        SpeechSynthesizer ss = new SpeechSynthesizer();

        public Speaker()
        {
            var voices = ss.GetInstalledVoices(new CultureInfo("zh-CN"));

            if (voices.Count == 0) voices = ss.GetInstalledVoices();

            ss.SelectVoice(voices.First().VoiceInfo.Name);
        }

        public void Say(string input)
        {
            ss.SpeakAsync(new Prompt(input, SynthesisTextFormat.Text));
        }
    }
}
