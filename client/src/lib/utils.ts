import { clsx, type ClassValue } from "clsx"
import { twMerge } from "tailwind-merge"

export function cn(...inputs: ClassValue[]) {
  return twMerge(clsx(inputs))
}

// Format number with decimal places
export function formatNumberWithDecimal(num: number): string {
  const [int, decimal] = num.toString().split('.');
  return decimal ? `${int}.${decimal.padEnd(2, '0')}` : `${int}.00`;
}

// Round number to 2 decimal places
export function round2(value: number | string) {
  if (typeof value === 'number') {
    return Math.round((value + Number.EPSILON) * 100) / 100;
  } else if (typeof value === 'string') {
    return Math.round((Number(value) + Number.EPSILON) * 100) / 100;
  } else {
    throw new Error('Value is not a number or string');
  }
}

const CURRENCY_FORMATTER = new Intl.NumberFormat('en-US', {
  currency: 'INR',
  style: 'currency',
  minimumFractionDigits: 2,
});

// Format currency using the formatter above
// amount is assumed to be in cents
export function formatCurrency(amount: number | string | null) {
  if (amount == null) return 'NaN';

  let value: number;

  if (typeof amount === 'number') {
    value = amount / 100; // convert cents to full units
  } else {
    value = Number(amount) / 100;
  }

  return CURRENCY_FORMATTER.format(value);
}


/* export function formatCurrency(amount: number | string | null) {
  if (typeof amount === 'number') {
    return CURRENCY_FORMATTER.format(amount);
  } else if (typeof amount === 'string') {
    return CURRENCY_FORMATTER.format(Number(amount));
  } else {
    return 'NaN';
  }
} */

// Format Number
const NUMBER_FORMATTER = new Intl.NumberFormat('en-US');

export function formatNumber(number: number) {
  return NUMBER_FORMATTER.format(number);
}
