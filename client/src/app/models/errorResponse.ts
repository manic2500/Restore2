export type ApiErrorResponse = {
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


