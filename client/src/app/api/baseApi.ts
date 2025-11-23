import { fetchBaseQuery, type BaseQueryApi, type FetchArgs, type FetchBaseQueryError } from "@reduxjs/toolkit/query";
import { startLoading, stopLoading } from "../store/slices/uiSlice";
import { toast } from "react-toastify";
import type { ApiErrorResponse } from "../models/errorResponse";

const customBaseQuery = fetchBaseQuery({
    baseUrl: 'https://localhost:5001/api'
});

type TypedFetchError = FetchBaseQueryError & {
    data?: ApiErrorResponse['data'];
};
//const sleep = () => new Promise(resolve => setTimeout(resolve, 1000));

export const baseQueryWithErrorHandling = async (args: string | FetchArgs, api: BaseQueryApi, extraOptions: object) => {
    // start loading
    api.dispatch(startLoading());
    //await sleep();
    const result = await customBaseQuery(args, api, extraOptions);
    //stop loading
    api.dispatch(stopLoading());
    if (result.error) {
        const error = result.error as TypedFetchError
        console.log(error);
        if (typeof error.status === "number") {
            switch (error.status) {
                case 400:
                case 401:
                    toast.error(error.data?.error.message ?? "Unknown error")
                    break;

                default:
                    break;
            }
        }
        else {
            // network / parsing / custom
            console.error("Non-HTTP error:", error);
        }

    }
    return result;
}