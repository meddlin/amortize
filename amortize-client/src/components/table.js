function Table({ tableData }) {

    return (
        <div className="Table">
            <h2>Amortization Table</h2>
            <table>
                <thead>
                <tr>
                    <th>Term</th>
                    <th>Monthly Payment</th>
                    <th>Principal</th>
                    <th>Interest</th>
                    <th>Remain. Pri.</th>
                    <th>Extra Payment</th>
                </tr>
                </thead>
                <tbody>
                {
                tableData && tableData.length > 0 ? 
                tableData.map( 
                    a => {
                        return (
                            <tr key={a.term}>
                                <td>{a.term}</td>
                                <td>{a.monthlyPayment}</td>
                                <td>{a.principal}</td>
                                <td>{a.interest}</td>
                                <td>{a.remainingPrincipal}</td>
                                <td>{a.extraPayment}</td>
                            </tr>
                        )
                    } 
                ) : 'No data'
                }
                </tbody>
            </table>
        </div>
    );
};

export default Table;