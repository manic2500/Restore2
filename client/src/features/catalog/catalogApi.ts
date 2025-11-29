import { baseQueryWithErrorHandling } from "@/app/api/baseApi";
import type { ApiResponse } from "@/app/models/ErrorResponse";
import type { Product } from "@/app/models/product";
import { createApi } from "@reduxjs/toolkit/query/react";


export const catalogApi = createApi({
    reducerPath: 'catalogApi',
    baseQuery: baseQueryWithErrorHandling,
    endpoints: (builder) => ({
        fetchProducts: builder.query<Product[], void>({
            query: () => ({
                url: 'products'
            })
        }),
        fetchProductDetails: builder.query<ApiResponse<Product>, string>({
            query: (productId) => `products/${productId}`
        })
    })
})

export const { useFetchProductDetailsQuery, useFetchProductsQuery } = catalogApi