import { fetchBaseQuery, type BaseQueryApi, type FetchArgs, type FetchBaseQueryError } from "@reduxjs/toolkit/query";
import { startLoading, stopLoading } from "../store/slices/uiSlice";
import { toast } from "react-toastify";
import type { RTKErrorResponse } from "../models/ErrorResponse";
import { router } from "../routes/Routes";


const baseApiQuery = fetchBaseQuery({
    baseUrl: 'https://localhost:5001/api',
    credentials: "include"
});

type TypedFetchError = FetchBaseQueryError & RTKErrorResponse;

//const sleep = () => new Promise(resolve => setTimeout(resolve, 1000));

export const baseQueryWithErrorHandling = async (args: string | FetchArgs, api: BaseQueryApi, extraOptions: object) => {
    // start loading
    api.dispatch(startLoading());
    //await sleep();
    const result = await baseApiQuery(args, api, extraOptions);
    //stop loading
    api.dispatch(stopLoading());
    if (result.error) {
        const { status, data } = result.error as TypedFetchError;
        //console.log(data.error.errors);
        if (typeof status === "number") {
            //console.log(data.success);
            const errData = data.error;
            switch (status) {
                case 400:
                    // Validation Errors                    
                    if (errData.errors) {
                        return {
                            error: {
                                status,
                                data: Object.values(errData.errors).flat(),
                            }
                        };
                    }
                    else {
                        toast.error(errData.message ?? "Unknown error")
                    }
                    break;
                case 401:
                    toast.error(errData.message ?? "Unknown error")
                    break;
                case 404:
                    router.navigate('/not-found')
                    break;
                case 500:
                    router.navigate('/server-error', { state: { error: errData } })
                    break
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