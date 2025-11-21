function AggregateCart() {
  const score = 48;
  return (
    <div className=" p-4 rounded-sm shadow-xs w-fit">
      <h3 className="font-semibold text-gray-700">Budget Balance</h3>
      <h2 className="text-3xl font-bold py-1">{score}</h2>
      <div className="w-40 h-2 bg-gray-200 rounded-lg my-2">
        <div
          className="h-2 bg-blue-700 rounded-lg transition-all duration-500"
          style={{ width: `${score}%` }}
        ></div>
      </div>
      <p className="text-sm text-gray-700">Your budget is well balanced</p>
    </div>
  );
}

export default AggregateCart;
