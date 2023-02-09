using System.ComponentModel;

namespace Demo.Enums
{
    public enum MessagesEnum
    {
        [Description("Successfully")]
        Success = 1,
        [Description("Failed !!  Please Enter Name, email, Department, Date of birth correctly again")]
        Failed = 2,
        [Description("Record not Exist in the System")]
        NotExist = 3
    }
}
