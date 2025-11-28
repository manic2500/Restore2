export type Basket = {
    basketId: string
    items: BasketItem[]
    subTotal: number
    discount: number
    shipping: number,
    tax: number,
    appliedVoucher?: string,
    grandTotal: number
}

export type BasketItem = {
    itemId: string
    productId: string
    name: string
    price: number
    pictureUrl: string
    brand: string
    type: string
    quantity: number
    totalPrice: number
}
