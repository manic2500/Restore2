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
        const { status, data } = result.error as TypedFetchError

        if (typeof status === "number") {
            switch (status) {
                case 400:
                    if (data?.error.errors) {
                        const errFlat = Object.values(data?.error.errors).flat();
                        return {
                            error: {
                                status,
                                data: errFlat,
                            }
                        };
                    }
                    else {
                        toast.error(data?.error.message ?? "Unknown error")
                    }
                    break;
                case 401:
                case 404:
                case 500:
                    toast.error(data?.error.message ?? "Unknown error")
                    break;
                default:
                    break;
            }
        }
        else {
            // network / parsing / custom
            console.error("Non-HTTP error:", result.error);
        }

    }
    return result;
}