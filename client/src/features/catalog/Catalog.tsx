import { useFetchProductsQuery } from "./catalogApi";
import ProductList from "./ProductList";


export default function Catalog() {

    const { data } = useFetchProductsQuery();

    if (!data) return null

    return (
        <>
            <ProductList products={data} />
        </>
    );
}