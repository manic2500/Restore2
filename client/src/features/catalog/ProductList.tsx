import type { Product } from "../../app/models/product";
import ProductCard from "./ProductCard";

export default function ProductList({ products }: { products: Product[] }) {
    return (
        <>
            {/* <div className="flex flex-wrap justify-center gap-6"> */}
            <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-4 gap-6">
                {products && products.map(product => (
                    <ProductCard key={product.productId} product={product} />
                ))}
            </div>

        </>
    );
}