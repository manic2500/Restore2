export type ApiErrorResponse = {
    status: number,
    data: {
        error: {
            message: string;
            errors?: Record<string, string[]>;
        }
    };
}

