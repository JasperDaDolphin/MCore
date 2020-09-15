namespace MCore.Base.Rpc {

    /// <summary>
    /// A collection of Rpc events
    /// </summary>
    public static class RpcEvents {

		public const string GetServerInformation = "Mcore:client:ready";
		public const string ClientDisconnect = "Mcore:client:disconnect";

		public const string GetUser = "Mcore:user:load";
		public const string GetCharacters = "Mcore:user:characters";
		public const string AcceptRules = "Mcore:user:rules";

		public const string CharacterCreate = "Mcore:character:create";
		public const string CharacterLoad = "Mcore:character:load";
		public const string CharacterDelete = "Mcore:character:delete";
		public const string CharacterSave = "Mcore:character:save";
		public const string CharacterRevive = "Mcore:character:revive";
		public const string GetCharacterPosition = "Mcore:character:getpos";

		public const string CharacterComponentSet = "Mcore:character:component:set";
		public const string CharacterPropSet = "Mcore:character:prop:set";
		
		public const string BankAtmWithdraw = "Mcore:bank:atm:withdraw";
		public const string BankBranchWithdraw = "Mcore:bank:branch:withdraw";
		public const string BankBranchDeposit = "Mcore:bank:branch:withdraw";
		public const string BankBranchTransfer = "Mcore:bank:branch:transfer";
		public const string BankOnlineTransfer = "Mcore:bank:online:transfer";

		public const string EntityDelete = "Mcore:entity:delete";

		public const string CarCreate = "Mcore:car:create";
		public const string CarSpawn = "Mcore:car:spawn";
		public const string CarSave = "Mcore:car:save";
		public const string CarTransfer = "Mcore:car:transfer";
		public const string CarClaim = "Mcore:car:claim";
		public const string CarUnclaim = "Mcore:car:unclaim";

		public const string BikeSpawn = "Mcore:bike:spawn";
		public const string BikeSave = "Mcore:bike:save";
		public const string BikeTransfer = "Mcore:bike:transfer";
		public const string BikeClaim = "Mcore:bike:claim";
		public const string BikeUnclaim = "Mcore:bike:unclaim";
	}
}
