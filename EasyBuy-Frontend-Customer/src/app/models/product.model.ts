export interface Product {
    id: number;
    productName: string;
    priceToSell: number;
    importPrice: number;
    discount: number;
    model: string;
    color: string;
    gender: string;
    description: string;
    productImg: string;
    canDel: number;
    stockQuantity: number;
    status: number;
    categoryId: number;
    code:string;
}
export interface productBuy {
    idProduct: number;
    quantity: number;
}
export interface productPayment{
    quantity: number,
    product: Product
}