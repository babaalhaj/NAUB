using NAUB.Library;
using NAUB.Models;

namespace NAUB.ViewModels
{
    public class OverdueViewModel
        {
            public OverdueDetails OverdueDetails { get; set; }
            public Borrow Borrow { get; set; }

            public bool AlreadyPaid { get; set; }
    }
    }