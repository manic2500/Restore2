import { Card, CardContent, CardHeader } from "@/components/ui/card";
import type { Product } from "../../app/models/product";
import ProductPrice from "./ProductPrice";
import { Link } from "react-router-dom";
import { Button } from "@/components/ui/button";


type Props = {
    product: Product
}

export default function ProductCard({ product }: Props) {
    return (
        <Card className="w-full max-w-sm p-1">
            <CardHeader className="p-0 items-center">
                <Link to={`/catalog/${product.publicId}`}>
                    <img src={product.pictureUrl} alt={product.name} className="w-full rounded-lg" />
                </Link>
            </CardHeader>
            <CardContent className="grid gap-4 px-4">
                <div className="text-lg font-black">
                    {product.brand}
                </div>
                <Link to={`/catalog/${product.publicId}`}>
                    <h2 className="text-sm font-medium">{product.name}</h2>
                </Link>
                <div className="flex-between">
                    <Button asChild variant={'default'} >
                        <Link to={`/catalog/${product.publicId}`}>Add to cart</Link>
                    </Button>
                    {product.quantityInStock > 0 ? (
                        <ProductPrice value={Number(product.price)} />
                    ) : <p className="text-destructive">
                        Out Of Stock
                    </p>}
                </div>
            </CardContent>
        </Card>
    );
};