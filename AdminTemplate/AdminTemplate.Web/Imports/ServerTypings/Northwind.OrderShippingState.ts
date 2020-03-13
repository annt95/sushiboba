namespace AdminTemplate.Northwind {
    export enum OrderShippingState {
        NotShipped = 0,
        Shipped = 1
    }
    Serenity.Decorators.registerEnumType(OrderShippingState, 'AdminTemplate.Northwind.OrderShippingState', 'Northwind.OrderShippingState');
}
