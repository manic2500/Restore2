import { Button } from "@/components/ui/button";
import { Loader, Trash2 } from "lucide-react";
import { toast } from "react-toastify";

import type { BasketItem } from "@/app/models/basket";
import { useRemoveBasketItemMutation } from "../basketApi";

export function DeleteButton({ item }: { item: BasketItem }) {
    const [removeItem, { isLoading }] = useRemoveBasketItemMutation();

    const handleDelete = async () => {
        const res = await removeItem({ itemId: item.itemId });
        if (res.error) {
            toast.error(res.data || "Failed to remove item");
        }
    };

    return (
        <Button
            variant="ghost"
            size="sm"
            disabled={isLoading}
            onClick={handleDelete}
            className="text-red-500"
        >
            {isLoading ? <Loader className="w-4 h-4 animate-spin" /> : <Trash2 className="w-4 h-4" />}
        </Button>
    );
}
