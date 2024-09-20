import React, { useState, useEffect } from "react";
import { useTable } from "react-table";
import { fetchUsers } from "../Services/UserService";
import './UserTable.css';

export const UserTable = () => {
    const [users, setUsers] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const loadUsers = async () => {
            try {
                const usersData = await fetchUsers();
                if (Array.isArray(usersData)) {
                    setUsers(usersData);
                } else {
                    console.error("Unexpected data format", usersData);
                    setUsers([]);
                }
            } catch (err) {
                console.error("Error fetching users", err);
                setError("Failed to load users");
            } finally {
                setLoading(false);
            }
        };

        loadUsers();
    }, []);

    const columns = React.useMemo(
        () => [
            {
                Header: 'Username',
                accessor: 'username',
            },
            {
                Header: 'Email',
                accessor: 'email',
            },
            {
                Header: 'Phone Number',
                accessor: 'phoneNumber',
            },
        ],
        []);

    // Only call useTable once, after loading is done
    const {
        getTableProps,
        getTableBodyProps,
        headerGroups,
        rows,
        prepareRow,
    } = useTable({ columns, data: users });

    if (loading) {
        return <div>Loading...</div>;
    }

    if (error) {
        return <div>{error}</div>;
    }

    return (
        <div>
            <h2>Users List</h2>
            <div className="tableDiv">

            <table {...getTableProps()} className="user-table">
                <thead>
                    {headerGroups.map(headerGroup => (
                        <tr {...headerGroup.getHeaderGroupProps()}>
                            {headerGroup.headers.map(column => (
                                <th {...column.getHeaderProps()}>{column.render('Header')}</th>
                            ))}
                        </tr>
                    ))}
                </thead>
                <tbody {...getTableBodyProps()}>
                    {rows.length > 0 ? (
                        rows.map(row => {
                            prepareRow(row);
                            return (
                                <tr {...row.getRowProps()}>
                                    {row.cells.map(cell => (
                                        <td {...cell.getCellProps()}>{cell.render('Cell')}</td>
                                    ))}
                                </tr>
                            );
                        })
                    ) : (
                        <tr>
                            <td colSpan="3">No users found</td>
                        </tr>
                    )}
                </tbody>
            </table>
        </div>
        </div>
    );
};
