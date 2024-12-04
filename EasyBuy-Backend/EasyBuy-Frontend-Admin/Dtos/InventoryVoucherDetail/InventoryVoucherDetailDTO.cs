namespace EasyBuy_Frontend_Admin.Dtos.InventoryVoucherDetail
{
    public class InventoryVoucherDetailDTO
    {
        public int SupplierId { get; set; }
        public DateTime Date { get; set; }

        public int ProductId { get; set; }
        public decimal ReceivingUnitPrice { get; set; }

        public int Quantity { get; set; }

    }
}
