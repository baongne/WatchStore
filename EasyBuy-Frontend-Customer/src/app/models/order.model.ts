import { Payment } from "./payment.model"
import { User } from "./user.model"
import { Voucher } from "./voucher.model"

export interface Order {
    id?: number,
    orderDate:Date,
    code: string,
    shippingFee:number,
    orderDiscount:number,
    orderTotal:number,
    address:string,
    userId:number,
    paymentId:number,
    voucherId?:number
    status:number
    user?:User,
    payment?:Payment,
    voucher?:Voucher
}