import { Link } from "react-router-dom";
import { useApplyVoucherMutation, useFetchBasketQuery, useRemoveVoucherMutation } from "./basketApi";
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table";
import { Card, CardContent, CardHeader } from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import { router } from "@/app/routes/Routes";
import { ArrowRight, Loader, X } from "lucide-react";
import { formatCurrency } from "@/lib/utils";
import { RemoveButton } from "./components/RemoveButton";
import { AddButton } from "./components/AddButton";
import { DeleteButton } from "./components/DeleteButton";
import { useRef } from "react";
import { toast } from "react-toastify";


export default function BasketPage() {
    const inputVoucherRef = useRef<HTMLInputElement>(null);
    const { data, isLoading } = useFetchBasketQuery();
    const [applyVoucher, { isLoading: isLoadingVoucher }] = useApplyVoucherMutation()
    const [removeVoucher, { isLoading: isLoadingRemoveVoucher }] = useRemoveVoucherMutation()

    const cart = data?.data;

    if (isLoading) return <div>Loading....</div>

    if (!cart) return <div>Basket is empty</div>

    const handleApplyVoucher = async () => {
        if (inputVoucherRef.current) {
            const value: string = inputVoucherRef.current.value;
            const res = await applyVoucher({ voucherCode: value });
            if (!res.error) {
                toast.success("Discount applied successfully");
                inputVoucherRef.current.value = '';
            }
        }
    };

    const handleRemoveVoucher = async () => {
        await removeVoucher();
    }

    const totalQuantity = cart.items.reduce((sum, item) => sum + item.quantity, 0);


    return (
        <>
            <h1 className='py-4 h2-bold'>Shopping Cart</h1>
            {!cart || cart.items.length === 0 ? (
                <div>
                    Cart is empty. <Link to='/'>Go Shopping</Link>
                </div>
            ) : (
                <div className="grid md:grid-cols-4 md:gap-5">

                    {/* CART ITEMS TABLE */}
                    <div className="overflow-x-auto md:col-span-3">
                        <Table>
                            <TableHeader>
                                <TableRow>
                                    <TableHead>Item</TableHead>
                                    <TableHead className="text-center">Quantity</TableHead>
                                    <TableHead className="text-right">Price</TableHead>
                                    <TableHead className="text-right">Total</TableHead>
                                    <TableHead className="text-center">Remove</TableHead>
                                </TableRow>
                            </TableHeader>
                            <TableBody>
                                {cart.items.map((item) => (
                                    <TableRow key={item.productId}>
                                        <TableCell>
                                            <Link to={`/catalog/${item.productId}`} className="flex items-center">
                                                <img src={item.pictureUrl} alt={item.name} width={50} height={50} />
                                                <span className="px-2">{item.name}</span>
                                            </Link>
                                        </TableCell>
                                        <TableCell className="flex-center gap-2">
                                            <RemoveButton item={item} />
                                            <span>{item.quantity}</span>
                                            <AddButton item={item} />
                                        </TableCell>
                                        <TableCell className="text-right">{formatCurrency(item.price)}</TableCell>
                                        <TableCell className="text-right font-medium">{formatCurrency(item.price * item.quantity)}</TableCell>
                                        <TableCell className="text-center">
                                            <DeleteButton item={item} />
                                        </TableCell>
                                    </TableRow>
                                ))}
                            </TableBody>
                        </Table>
                    </div>

                    {/* ORDER SUMMARY CARD */}
                    <div className="space-y-4 md:col-span-1">
                        <Card>
                            <CardHeader>
                                <span className='h3-bold'>Order Summary</span>
                            </CardHeader>
                            <CardContent className="p-4 space-y-4">
                                {/* ITEMS SUBTOTAL */}
                                <div className="flex justify-between items-baseline text-lg">
                                    <div className="flex items-baseline gap-2">
                                        <span>Subtotal</span>
                                        <span className="text-sm text-muted-foreground">({totalQuantity} items)</span>
                                    </div>
                                    <span className="font-bold">{formatCurrency(cart.subTotal)}</span>
                                </div>

                                {/* TAX */}
                                <div className="flex justify-between text-sm text-muted-foreground">
                                    <span>Tax (8%)</span>
                                    <span>{formatCurrency(cart.tax)}</span>
                                </div>

                                {/* SHIPPING */}
                                <div className="flex justify-between text-sm text-muted-foreground">
                                    <span>Shipping</span>
                                    <span>{formatCurrency(cart.shipping)}</span>
                                </div>

                                {/* DISCOUNT */}
                                <div className="flex justify-between text-sm text-muted-foreground">
                                    <span>Discount</span>
                                    <span>{formatCurrency(0)}</span>
                                </div>

                                <hr />
                                {/* VOUCHER DISCOUNT — show only if applied */}
                                {cart.appliedVoucher && (
                                    <>
                                        <div className="flex justify-between text-sm text-muted-foreground mb-1">
                                            <span>Coupon Discount</span>
                                            <span>{formatCurrency(cart.discount)}</span>
                                        </div>
                                        <div className="inline-flex items-center  bg-gray-100 dark:bg-gray-800 text-gray-800 dark:text-gray-100  text-sm font-semibold rounded-full px-3 py-1 space-x-2">
                                            {/* Voucher code with dotted underline */}
                                            <span className="border-b border-dotted border-current">
                                                {cart.appliedVoucher}
                                            </span>

                                            {/* Remove button using shadcn Button */}
                                            <Button
                                                asChild
                                                onClick={handleRemoveVoucher}
                                                disabled={isLoadingRemoveVoucher}
                                                variant="destructive"
                                                className="p-1 w-5 h-5 flex items-center justify-center rounded-full"
                                            >
                                                <X strokeWidth={4} />
                                            </Button>
                                        </div>
                                        <hr />
                                    </>
                                )}

                                {/* GRAND TOTAL */}
                                <div className="flex justify-between text-xl py-2">
                                    <span>Grand Total</span>
                                    <span className="font-bold">{formatCurrency(cart.grandTotal)}</span>
                                </div>

                                <Button
                                    className="w-full"
                                    disabled={isLoading}
                                    onClick={() => router.navigate("/shipping-address")}
                                >
                                    {isLoading ? <Loader className="w-4 h-4 animate-spin" /> : <ArrowRight className="w-4 h-4" />}
                                    &nbsp;Proceed to Checkout
                                </Button>
                            </CardContent>
                        </Card>

                        {/* VOUCHER CODE CARD */}
                        <Card>
                            <CardContent className="p-4 space-y-3">
                                <div className="flex flex-col gap-2">
                                    <label htmlFor="voucher" className="text-sm font-medium text-muted-foreground">
                                        Have a voucher code?
                                    </label>
                                    <input
                                        ref={inputVoucherRef}
                                        id="voucher"
                                        type="text"
                                        disabled={isLoadingVoucher}
                                        placeholder="Enter code"
                                        className="border rounded px-3 py-2 text-sm w-full"
                                    />
                                </div>
                                <Button onClick={handleApplyVoucher} className="w-full">Apply Code</Button>
                            </CardContent>
                        </Card>
                    </div>
                </div>

            )}
        </>
    );
}