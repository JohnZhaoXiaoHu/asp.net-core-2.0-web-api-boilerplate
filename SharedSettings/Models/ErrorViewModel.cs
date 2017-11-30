namespace SharedSettings.Models
{
    public class ErrorViewModel
    {
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string InnerMessage { get; set; }

        public string TranslatedMessage
        {
            get
            {
                if (InnerMessage != null)
                {
                    if (InnerMessage.StartsWith("Cannot insert duplicate key row in object") && InnerMessage.Contains("with unique index"))
                    {
                        return "不能插入重复的数据(索引重复)";
                    }
                    if (InnerMessage.StartsWith("The DELETE statement conflicted with the REFERENCE constraint"))
                    {
                        return "该数据有下属关联数据, 无法删除. 请将下属关联数据删除后再进行操作";
                    }
                }
                return InnerMessage;
            }
        }
    }
}
