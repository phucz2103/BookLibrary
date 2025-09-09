import type { FieldError, UseFormRegisterReturn } from "react-hook-form";

interface FormSelectProps {
    id: string;
    label: string;
    options: { value: string; label: string }[];
    register: UseFormRegisterReturn;
    error?: FieldError;
}

const FormSelect: React.FC<FormSelectProps> = ({ id, label, options, register, error }) => {
    return (
        <div className="mb-4">
            <label className="block text-blue-400 mb-2 text-left" htmlFor={id}>
                {label}
            </label>
            <select id={id} {...register} className="w-full p-2 border rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500  bg-red-100">
                {options.map((option) => (
                    <option key={option.value} value={option.value}>
                        {option.label}
                    </option>
                ))}
            </select>
            {error && <p className="text-red-500 text-sm mt-1">{error.message}</p>}
        </div>
    );
}
export default FormSelect;