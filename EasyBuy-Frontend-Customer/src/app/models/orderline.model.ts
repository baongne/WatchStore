import { Order } from "./order.model";
import { Product } from "./product.model";

export interface Orderline {
    id?: number,
    quantity: number,
    unitPrice: number,
    orderId:number,
    productId:number,
    product ?:Product
}