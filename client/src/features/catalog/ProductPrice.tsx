import { APP_CURRENCY } from "@/lib/constants";
import { cn } from "@/lib/utils";

export default function ProductPrice({ value, className }: { value: number, className?: string }) {
    // Ensure two decimal places
    const stringValue = (value / 100).toFixed(2);
    // Get the int/float
    const [intValue, floatValue] = stringValue.split('.')

    return (
        <p className={cn('text-2xl', className)}>
            <span className="text-sm align-super">{APP_CURRENCY}</span>
            <span className="font-bold">{intValue}</span>
            <span className="text-sm align-super">.{floatValue}</span>
        </p>
    );
};