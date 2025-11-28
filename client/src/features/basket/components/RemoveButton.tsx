import type { BasketItem } from "@/app/models/basket";
import { useDecBasketItemMutation } from "../basketApi";
import { Button } from "@/components/ui/button";
import { toast } from "react-toastify";
import { Loader, Minus } from "lucide-react";

export function RemoveButton({ item }: { item: BasketItem }) {

    const [decrementItem, { isLoading }] = useDecBasketItemMutation();
    return (
        <Button
            disabled={isLoading}
            variant='outline'
            type='button'
            onClick={async () => {
                const res = await decrementItem({ itemId: item.itemId });
                if (res.error) {
                    toast.error(res.data);
                }
            }
            }
        >
            {isLoading ? (
                <Loader className='w-4 h-4 animate-spin' />
            ) : (
                <Minus className='w-4 h-4' />
            )}
        </Button>
    );
}