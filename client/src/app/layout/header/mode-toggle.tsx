import { useState, useEffect } from "react";
import { Button } from "@/components/ui/button";
import {
    DropdownMenu,
    DropdownMenuTrigger,
    DropdownMenuLabel,
    DropdownMenuSeparator,
    DropdownMenuContent,
    DropdownMenuCheckboxItem,
} from "@/components/ui/dropdown-menu";
import { MoonIcon, SunIcon, SunMoon } from "lucide-react";

// Optional: utility to persist theme in localStorage
const getInitialTheme = () => {
    if (typeof window !== "undefined") {
        return localStorage.getItem("theme") || "system";
    }
    return "system";
};

export default function ModeToggle() {
    const [mounted, setMounted] = useState(false);
    const [theme, setTheme] = useState(getInitialTheme);

    // Handle mount (so SSR issues in Next.js don’t exist here)
    useEffect(() => {
        // eslint-disable-next-line react-hooks/set-state-in-effect
        setMounted(true);
    }, []);

    // Apply theme class to document body
    useEffect(() => {
        if (!mounted) return;

        if (theme === "dark") {
            document.documentElement.classList.add("dark");
        } else {
            document.documentElement.classList.remove("dark");
        }

        // Persist theme in localStorage
        localStorage.setItem("theme", theme);
    }, [theme, mounted]);

    if (!mounted) return null;

    return (
        <DropdownMenu>
            <DropdownMenuTrigger asChild>
                <Button variant="ghost" className="focus-visible:ring-0 focus-visible:ring-offset-0">
                    {theme === "system" ? (
                        <SunMoon />
                    ) : theme === "dark" ? (
                        <MoonIcon />
                    ) : (
                        <SunIcon />
                    )}
                </Button>
            </DropdownMenuTrigger>

            <DropdownMenuContent>
                <DropdownMenuLabel>Appearance</DropdownMenuLabel>
                <DropdownMenuSeparator />

                <DropdownMenuCheckboxItem checked={theme === "system"} onClick={() => setTheme("system")}>
                    System
                </DropdownMenuCheckboxItem>

                <DropdownMenuCheckboxItem checked={theme === "dark"} onClick={() => setTheme("dark")}>
                    Dark
                </DropdownMenuCheckboxItem>

                <DropdownMenuCheckboxItem checked={theme === "light"} onClick={() => setTheme("light")}>
                    Light
                </DropdownMenuCheckboxItem>
            </DropdownMenuContent>
        </DropdownMenu>
    );
}
