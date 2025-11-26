import { baseQueryWithErrorHandling } from "@/app/api/baseApi";
import type { Basket } from "@/app/models/basket";
import { createApi } from "@reduxjs/toolkit/query/react";

export const basketApi = createApi({
    reducerPath: 'basketApi',
    baseQuery: baseQueryWithErrorHandling,
    endpoints: (builder) => ({
        fetchBasket: builder.query<Basket, void>({
            query: () => 'basket'
        }),
        addBasketItem: builder.mutation<Basket, { productId: string, quantity: number }>({
            query: ({ productId, quantity }) => ({
                url: 'basket',
                method: 'POST',
                body: { productId, quantity }
            })
        }),
        removeBasketItem: builder.mutation<void, { itemId: string }>({
            query: ({ itemId }) => ({
                url: `basket/items/${itemId}`,
                method: 'DELETE'
            })
        })
    })
})

export const { useFetchBasketQuery, useAddBasketItemMutation, useRemoveBasketItemMutation } = basketApi