import { Button } from "@/components/ui/button";
import { TriangleAlert } from "lucide-react";
import { Link, useLocation } from "react-router-dom";

export default function ServerError() {
    const { state } = useLocation()
    const isProd = import.meta.env.MODE === "production"
    const showStack = !isProd && state?.error?.stackTrace

    return (
        <div className="flex flex-col items-center min-h-screen px-4 py-5">
            <TriangleAlert size={70} className="text-red-500" />

            <div className="p-6 rounded-lg shadow-md text-center bg-background">
                <h3 className="text-3xl font-bold mb-4">Something went wrong</h3>

                {showStack ? (
                    <pre className="text-destructive w-fit text-left whitespace-pre-wrap text-sm bg-red-50 p-4 rounded-md border border-red-200">
                        {state.error.stackTrace}
                    </pre>
                ) : (
                    <p className="text-muted-foreground text-center">
                        An unexpected error occurred. Please try again later.
                    </p>
                )}

                <Button asChild className="mt-6">
                    <Link to="/">Back to Home</Link>
                </Button>
            </div>
        </div>
    );
}