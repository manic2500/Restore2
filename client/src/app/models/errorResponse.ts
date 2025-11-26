
export interface RTKErrorResponse {
    status: number
    data: {
        success: boolean
        error: ErrorData
    }
}
interface ErrorData {
    details: string;
    statusCode: number;
    message: string;
    errors: string[] | null;
    stackTrace: string;
}

export interface ValidationErrorResponse {
    status: number;
    data: string[];
}
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



