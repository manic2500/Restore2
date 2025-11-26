export type Basket = {
    basketId: string
    items: Item[]
    totalAmount: number
}

export type Item = {
    productId: string
    name: string
    price: number
    pictureUrl: string
    brand: string
    type: string
    quantity: number
    totalPrice: number
}
