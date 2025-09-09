import React from "react";
import type { FieldError, UseFormRegisterReturn } from 'react-hook-form';

interface FormInputProps {
    id: string;
    label: string;
    type : string;
    register : UseFormRegisterReturn;
    error?: FieldError;
}

const FormInput: React.FC<FormInputProps> = ({ id, label, type, register, error}) =>{
    return(
        <div className="mb-4">
            <label className="block text-blue-400 mb-2 text-left" htmlFor={id}>
                {label}
            </label>
            <input type={type} id={id} {...register} className="w-full p-2 border rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500 bg-red-100"/>
            {error && <p className="text-red-500 text-sm mt-1">{error.message}</p>}
        </div>
    );
};

export default FormInput;