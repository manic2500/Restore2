import { useFetchBasketQuery } from "./basketApi";

export default function BasketPage() {

    const { data, isLoading } = useFetchBasketQuery();

    if (isLoading) return <div>Loading....</div>

    if (!data) return <div>Basket is empty</div>

    return (
        <div>BasketPage</div>
    );
}