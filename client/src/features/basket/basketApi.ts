import { baseQueryWithErrorHandling } from "@/app/api/baseApi";
import type { Basket } from "@/app/models/basket";
import { createApi } from "@reduxjs/toolkit/query/react";

export const basketApi = createApi({
    reducerPath: 'basketApi',
    baseQuery: baseQueryWithErrorHandling,
    tagTypes: ["Basket"],
    endpoints: (builder) => ({
        fetchBasket: builder.query<Basket, void>({
            query: () => 'basket',
            providesTags: ["Basket"]
        }),
        addBasketItem: builder.mutation<Basket, { productId: string, quantity: number }>({
            query: ({ productId, quantity }) => ({
                url: 'basket',
                method: 'POST',
                body: { productId, quantity }
            }),
            invalidatesTags: ["Basket"],
        }),
        removeBasketItem: builder.mutation<void, { itemId: string }>({
            query: ({ itemId }) => ({
                url: `basket/items/${itemId}`,
                method: 'DELETE'
            }),
            invalidatesTags: ["Basket"],
        }),
        incBasketItem: builder.mutation<void, { itemId: string }>({
            query: ({ itemId }) => ({
                url: `basket/items/${itemId}/increment`,
                method: 'POST'
            }),
            invalidatesTags: ["Basket"],
        }),
        decBasketItem: builder.mutation<void, { itemId: string }>({
            query: ({ itemId }) => ({
                url: `basket/items/${itemId}/decrement`,
                method: 'POST'
            }),
            invalidatesTags: ["Basket"],
        }),
        applyVoucher: builder.mutation<void, { voucherCode: string }>({
            query: ({ voucherCode }) => ({
                url: `basket/apply-voucher`,
                method: 'POST',
                body: { voucherCode }
            }),
            invalidatesTags: ["Basket"],
        }),
        removeVoucher: builder.mutation<void, void>({
            query: () => ({
                url: `basket/remove-voucher`,
                method: 'DELETE'
            }),
            invalidatesTags: ["Basket"],
        }),

    })
})

export const { useFetchBasketQuery,
    useAddBasketItemMutation,
    useRemoveBasketItemMutation,
    useIncBasketItemMutation,
    useDecBasketItemMutation,
    useApplyVoucherMutation,
    useRemoveVoucherMutation,
} = basketApi