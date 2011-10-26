using System.Collections.Generic;

namespace FabrikamWidgets.Core.Services
{
    public struct CustomerSnapshot
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public List<int> MonthlyOrders { get; set; }
    }
}
