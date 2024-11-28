import { Product } from "./product.model";
import { User } from "./user.model";

export interface Cart{
    id?: number;
    quantity: number;
    userId:number;
    //user:User,
    productId:number;
   // product:Product
}
export interface CartShow{
    id?: number;
    quantity: number;
    userId:number;
    productId:number;
    product:Product
}