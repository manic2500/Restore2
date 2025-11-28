//import { APP_CURRENCY } from "@/lib/constants";
import { cn, formatCurrency } from "@/lib/utils";

export default function ProductPrice({ value, className }: { value: number, className?: string }) {
    // Get formatted currency string from the centralized formatter
    const formatted = formatCurrency(value); // e.g. "₹1,234.56"

    // Split symbol, integer, fraction for custom styling
    const match = formatted.match(/^(\D*)([\d,]+)\.(\d{2})$/);
    const symbol = match ? match[1] : '';
    const intPart = match ? match[2] : '0';
    const fraction = match ? match[3] : '00';

    return (
        <p className={cn('text-2xl', className)}>
            <span className="text-sm align-super">{symbol}</span>
            <span className="font-bold">{intPart}</span>
            <span className="text-sm align-super">.{fraction}</span>
        </p>
    );
    /* // Ensure two decimal places
    const stringValue = (value / 100).toFixed(2);
    // Get the int/float
    const [intValue, floatValue] = stringValue.split('.')

    return (
        <p className={cn('text-2xl', className)}>
            <span className="text-sm align-super">{APP_CURRENCY}</span>
            <span className="font-bold">{intValue}</span>
            <span className="text-sm align-super">.{floatValue}</span>
        </p>
    ); */
};