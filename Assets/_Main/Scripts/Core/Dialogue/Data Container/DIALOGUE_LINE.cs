namespace Dialogue
{
    // contain a single line that is converted to current action
    public class DIALOGUE_LINE
    {
        public string speaker;
        public string content;
        public string command;

        public DIALOGUE_LINE( string speaker, string content, string command )
        {
            this.speaker = speaker;
            this.content = content;
            this.command = command;
        }

        public bool hasContent => content.Trim().Length > 0;
        public bool hasCommand => command.Trim().Length > 0;
        public bool hasSpeaker => speaker.Trim().Length > 0;

	}
}