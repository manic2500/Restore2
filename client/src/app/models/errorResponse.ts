
export interface RTKApiResponse<T> {
    status: number
    data: ApiResponse<T>
}

export interface ApiResponse<T> {
    success: boolean
    status: number,
    data: T
    error: string,
    errors: string[]
}

/* interface ErrorData {
    details: string;
    statusCode: number;
    message: string;
    errors: string[] | null;
    stackTrace: string;
}

export interface ValidationErrorResponse {
    status: number;
    data: string[];
} */
/* Sample data - ValidationErrorResponse
{
    "status": 400,
    "data": [
        "This is the first error",
        "This is the second error"
    ]
}
 */

/* export type ApiErrorResponse = {
    status: number,
    data: ErrorDetail;
}

export type ErrorDetail = {
    error: {
        message: string;
        exceptionType: string;
        errors?: Record<string, string[]>;
    }
}
 */



