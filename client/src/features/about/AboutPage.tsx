import { useState } from "react";
import { useLazyGet400ErrorQuery, useLazyGet401ErrorQuery, useLazyGet404ErrorQuery, useLazyGet500ErrorQuery, useLazyGetValidationErrorQuery } from "./errorApi";
import { Button } from "@/components/ui/button";

export default function AboutPage() {
    const [validationErrors, setValidationErrors] = useState<string[]>([]);

    const [trigger400Error] = useLazyGet400ErrorQuery();
    const [trigger401Error] = useLazyGet401ErrorQuery();
    const [trigger404Error] = useLazyGet404ErrorQuery();
    const [trigger500Error] = useLazyGet500ErrorQuery();
    const [triggerValidationError] = useLazyGetValidationErrorQuery();

    const getValidatonError = async () => {

        try {
            await triggerValidationError().unwrap();
        } catch (err: any) {
            console.log("Caught validation errors:", err.data); // err.data is the flattened array
            setValidationErrors(err.data); // display in UI
        }
        /* triggerValidationError().unwrap().catch(error => {
            const errorArray = (error as { message: string }).message.split(',');
            setValidationErrors(errorArray);
        }); */
    };


    return (
        <div className="max-w-7xl mx-auto p-4 space-y-4">
            <h3 className="text-3xl font-semibold">Errors for testing</h3>

            <div className="flex flex-col sm:flex-row sm:space-x-2 space-y-2 sm:space-y-0 w-full">
                <Button
                    className="flex-1"
                    onClick={() =>
                        trigger400Error().catch((err) => console.log(err))
                    }
                >
                    Test 400 Error
                </Button>
                <Button
                    className="flex-1"
                    onClick={() =>
                        trigger401Error().catch((err) => console.log(err))
                    }
                >
                    Test 401 Error
                </Button>
                <Button
                    className="flex-1"
                    onClick={() =>
                        trigger404Error().catch((err) => console.log(err))
                    }
                >
                    Test 404 Error
                </Button>
                <Button
                    className="flex-1"
                    onClick={() =>
                        trigger500Error().catch((err) => console.log(err))
                    }
                >
                    Test 500 Error
                </Button>
                <Button className="flex-1" onClick={getValidatonError}>
                    Test Validation Error
                </Button>
            </div>
            Length is - {validationErrors.length}
            {validationErrors.length > 0 && (
                <ul>
                    {validationErrors.map(err => <li key={err}>{err}</li>)}
                </ul>)}

            {/* {validationErrors.length > 0 && (
        <Alert variant="destructive">
          <AlertTitle>Validation errors</AlertTitle>
          <ScrollArea className="max-h-60 mt-2">
            <ul className="list-disc pl-4 space-y-1">
              {validationErrors.map((err) => (
                <li key={err}>{err}</li>
              ))}
            </ul>
          </ScrollArea>
        </Alert>
      )} */}
        </div>
    );
}