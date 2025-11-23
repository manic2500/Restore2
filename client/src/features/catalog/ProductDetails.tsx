import { Button } from "@/components/ui/button";
import { Card } from "@/components/ui/card";
import { Input } from "@/components/ui/input";
import { Table, TableBody, TableCell, TableRow } from "@/components/ui/table";
import { APP_CURRENCY } from "@/lib/constants";
import { useParams } from "react-router-dom";
import { useFetchProductDetailsQuery } from "./catalogApi";

export default function ProductDetails() {
    const { id } = useParams();

    const { data: product } = useFetchProductDetailsQuery(id ? id : '');

    if (!product) return null

    const productDetails = [
        { label: 'Name', value: product.name },
        { label: 'Description', value: product.description },
        { label: 'Type', value: product.type },
        { label: 'Brand', value: product.brand },
        { label: 'Quantity in stock', value: product.quantityInStock },
    ]

    return (
        <div className="container mx-auto max-w-screen-lg">
            <div className="grid grid-cols-1 md:grid-cols-2 gap-6">

                {/* Image */}
                <div>
                    <img
                        src={product?.pictureUrl}
                        alt={product.name}
                        className="w-full object-cover rounded-md"
                    />
                </div>

                {/* Right Column */}
                <div>
                    {/* Title */}
                    <h1 className="text-3xl font-bold">{product.name}</h1>
                    <div className="border-b my-2"></div>

                    {/* Price */}
                    <p className="text-2xl font-semibold">
                        {APP_CURRENCY}{(product.price / 100).toFixed(2)}
                    </p>

                    {/* Details Table */}
                    <Card className="mt-4 p-0 overflow-hidden">
                        <Table>
                            <TableBody>
                                {productDetails.map((detail, index) => (
                                    <TableRow key={index}>
                                        <TableCell className="font-bold text-base">
                                            {detail.label}
                                        </TableCell>
                                        <TableCell className="text-base whitespace-normal wrap-break-word">
                                            {detail.value}
                                        </TableCell>
                                    </TableRow>
                                ))}
                            </TableBody>
                        </Table>
                    </Card>

                    {/* Quantity + Button */}
                    <div className="grid grid-cols-1 sm:grid-cols-2 gap-4 mt-6">
                        {/* Quantity Input */}
                        <Input
                            type="number"
                            defaultValue={1}
                            className="h-[55px]"
                            placeholder="Quantity in basket"
                        />

                        {/* Add to Basket Button */}
                        <Button
                            className="h-[55px] w-full"
                            size="lg"
                        >
                            Add to Basket
                        </Button>
                    </div>
                </div>
            </div>
        </div>
    );
}