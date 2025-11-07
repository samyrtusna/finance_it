import React from "react";

type InputFieldProps = {
  labelName: string;
  icon: React.ElementType;
  id: string;
  name: string;
  type: string;
  value: string;
  handleChange: (e: React.ChangeEvent<HTMLInputElement>) => void;
  placeholder: string;
};

function InputField({
  labelName,
  icon: Icon,
  id,
  name,
  type,
  value,
  placeholder,
}: InputFieldProps) {
  return (
    <div className="mb-2">
      <label
        htmlFor={id}
        className="block text-sm font-semibold"
      >
        {labelName}
      </label>
      <div className="flex items-center w-full rounded-md shadow-lg p-2">
        <Icon className="size-6 text-gray-400" />

        <input
          id={id}
          name={name}
          type={type}
          value={value}
          placeholder={placeholder}
          className="w-full p-2 outline-none focus:placeholder-transparent"
        />
      </div>
    </div>
  );
}

export default InputField;
