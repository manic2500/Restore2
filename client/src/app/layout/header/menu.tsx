import { Button } from "@/components/ui/button";
import ModeToggle from "./mode-toggle";

import { EllipsisVertical, ShoppingCart, UserIcon } from "lucide-react";
import { Sheet, SheetContent, SheetDescription, SheetTitle, SheetTrigger } from "@/components/ui/sheet";
import { Link } from "react-router-dom";
import { useFetchBasketQuery } from "@/features/basket/basketApi";


export default function Menu() {

    const { data: basket } = useFetchBasketQuery();

    const totalQuantity = basket?.items.reduce((sum, item) => sum + item.quantity, 0) ?? 0;

    return (
        <div className="flex justify-end gap-3">
            <nav className="hidden md:flex w-full max-w-xs gap-1">
                <ModeToggle />
                <Button asChild variant="ghost" className="relative">
                    <Link to="/basket" className="flex items-center">
                        <div className="relative">
                            <ShoppingCart className="w-6 h-6" />
                            {totalQuantity > 0 && (
                                <span className="absolute -top-3 -right-4 bg-red-500 text-white text-xs font-bold rounded-full w-5 h-5 flex items-center justify-center">
                                    {totalQuantity}
                                </span>
                            )}
                        </div>
                        <span className="ml-2">Cart</span>
                    </Link>
                </Button>

                <Button asChild>
                    <Link to={'/sign-in'}>
                        <UserIcon /> Sign In
                    </Link>
                </Button>
            </nav>
            <nav className="md:hidden">
                <Sheet>
                    <SheetTrigger className="align-middle">
                        <EllipsisVertical />
                    </SheetTrigger>
                    <SheetContent className="flex flex-col items-start">
                        <SheetTitle>Menu</SheetTitle>
                        <ModeToggle />
                        <Button asChild variant={'ghost'}>
                            <Link to={'/basket'}>
                                <ShoppingCart /> Cart ({totalQuantity})
                            </Link>
                        </Button>
                        <Button asChild>
                            <Link to={'/sign-in'}>
                                <UserIcon /> Sign In
                            </Link>
                        </Button>
                        <SheetDescription></SheetDescription>
                    </SheetContent>
                </Sheet>
            </nav>
        </div>
    );
};