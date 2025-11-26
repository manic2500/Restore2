import { baseQueryWithErrorHandling } from "@/app/api/baseApi";
import type { Product } from "@/app/models/product";
import { type SuccessResponse } from "@/app/models/SuccessResponse";
import { createApi } from "@reduxjs/toolkit/query/react";


export const catalogApi = createApi({
    reducerPath: 'catalogApi',
    baseQuery: baseQueryWithErrorHandling,
    endpoints: (builder) => ({
        fetchProducts: builder.query<SuccessResponse<Product[]>, void>({
            query: () => ({
                url: 'products'
            })
        }),
        fetchProductDetails: builder.query<SuccessResponse<Product>, string>({
            query: (productId) => `products/${productId}`
        })
    })
})

export const { useFetchProductDetailsQuery, useFetchProductsQuery } = catalogApi