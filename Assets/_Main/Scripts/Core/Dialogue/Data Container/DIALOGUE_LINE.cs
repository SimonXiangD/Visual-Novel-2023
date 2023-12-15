using UnityEngine;


namespace Dialogue
{
    // contain a single line that is converted to current action
    public class DIALOGUE_LINE
    {
        public string speaker;
        public string content;
        public string command;
        public DL_SPEAKER speakerInfo;
        public string speakerName => speakerInfo.speakerName;
		

		public DIALOGUE_LINE( string speaker, string content, string command )
        {
            this.speaker = speaker;
            this.content = content;
            this.command = command;
			this.speakerInfo = new DL_SPEAKER(speaker);
            show();
        }

        public bool hasContent => content.Trim().Length > 0;
        public bool hasCommand => command.Trim().Length > 0;
        public bool hasSpeaker => speaker.Trim().Length > 0;

        public void show()
        {
            Debug.Log(speaker);
            Debug.Log(content);
            Debug.Log(command);
            speakerInfo.show();
		}
	}


}