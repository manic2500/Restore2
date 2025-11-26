


export interface SuccessResponse<T> {
    success: boolean;
    data: T;
    statusCode: number;
    httpStatus: number;
    message: string;
}
