import { Button } from "@/components/ui/button";
import { AlertTriangle } from "lucide-react";
import { Link } from "react-router-dom";

export default function NotFoundPage() {
    return (
        <div className="flex h-screen flex-col items-center text-center px-4">
            <AlertTriangle size={70} className="text-muted-foreground mb-4" />

            <h1 className="text-3xl font-bold">Page Not Found</h1>
            <p className="text-muted-foreground mt-2 max-w-sm">
                The page you're looking for doesn't exist or may have been moved.
            </p>

            <Button asChild className="mt-6">
                <Link to="/">Go Back Home</Link>
            </Button>
        </div>
    );
};