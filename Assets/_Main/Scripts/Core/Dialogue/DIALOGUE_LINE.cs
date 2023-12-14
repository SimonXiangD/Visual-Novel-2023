namespace Dialogue
{
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


    }
}