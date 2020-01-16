namespace NAUB.ViewModels
    {
    public class ExtendViewModel
        {
            public string BorrowerType { get; set; }
            public string BorrowerId { get; set; }
            public string Name { get; set; }
            public string Status() => (BorrowerType == "1") ? "Staff" : "Student";
            public int NumberOfDays { get; set; }
        }
    }