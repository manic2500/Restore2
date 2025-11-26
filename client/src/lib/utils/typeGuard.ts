import type { ValidationErrorResponse } from "@/app/models/ErrorResponse";

export function isValidationErrorResponse(err: unknown): err is ValidationErrorResponse {
    if (typeof err !== "object" || err === null) return false;

    const obj = err as Record<string, unknown>;

    // Check status
    if (typeof obj.status !== "number") return false;

    // Check data is an array of strings
    if (!Array.isArray(obj.data)) return false;
    if (!obj.data.every(item => typeof item === "string")) return false;

    return true;
}

// When return the complete "result" object in global error handling - baseApi
// Interface to validate is "ErrorResponse"
/* function isErrorResponse(err: unknown): err is ErrorResponse {
    if (
        typeof err !== "object" ||
        err === null
    ) {
        return false;
    }

    const obj = err as Record<string, unknown>;

    // Check status
    if (typeof obj.status !== "number") {
        return false;
    }

    // Check data and data.error
    const data = obj.data as Record<string, unknown> | undefined;
    if (
        typeof data !== "object" ||
        data === null ||
        typeof data.error !== "object" ||
        data.error === null
    ) {
        return false;
    }

    return true;
} */