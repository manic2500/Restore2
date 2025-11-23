import { useAppDispatch, useAppSelector } from "@/app/store/hooks";
import { decrement, increment } from "@/app/store/slices/counterSlice";

import { Button } from "@/components/ui/button";


export default function ContactPage() {
    const counter = useAppSelector((state) => state.counter.data)
    const dispatch = useAppDispatch();
    return (
        <div>
            Counter {counter}
            <Button onClick={() => dispatch(increment())}>+</Button>
            <Button onClick={() => dispatch(decrement())}>-</Button>
            <br />
            <Button onClick={() => dispatch(increment(5))}>+5</Button>
            <Button onClick={() => dispatch(decrement(3))}>-3</Button>
        </div>
    );
}