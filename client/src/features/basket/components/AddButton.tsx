import type { BasketItem } from "@/app/models/basket";
import { useIncBasketItemMutation } from "../basketApi";
import { Button } from "@/components/ui/button";
import { toast } from "react-toastify";
import { Loader, Plus } from "lucide-react";

export function AddButton({ item }: { item: BasketItem }) {

    const [incrementItem, { isLoading }] = useIncBasketItemMutation();
    return (
        <Button
            disabled={isLoading}
            variant='outline'
            type='button'
            onClick={async () => {
                const res = await incrementItem({ itemId: item.itemId });
                if (res.error) {
                    toast.error(res.data);
                }
            }
            }
        >
            {isLoading ? (
                <Loader className='w-4 h-4 animate-spin' />
            ) : (
                <Plus className='w-4 h-4' />
            )}
        </Button>
    );
}