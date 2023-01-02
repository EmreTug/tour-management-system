namespace CappadociaTour.API.Models
{
    public class Reservation
    {

        public Reservation()
        {
            Customer = new HashSet<CustomerDetail>();
           
        }
        public string ReservationDate { get; set; }
        public string CreatedDate { get; set; }
        public int Pax { get; set; }
        public int Status { get; set; }
        public int variantGroupId { get; set; }

        public int CustomerRoomNumber { get; set; }
        public decimal CustomerPayment { get; set; }
        public int currencyTypeId { get; set; }

        public string OperationNote { get; set; }
        public string Note { get; set; }
        public virtual ICollection<CustomerDetail> Customer { get; set; }



    }
}
