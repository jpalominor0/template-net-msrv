namespace FNT_Application.Features
{
    public class MenuHandler
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        
        public MenuHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;            
        }

        public async Task<MenuDTO> PutHandler(MenuDTO MenuDTO)
        {
            return _mapper.Map<MenuDTO>(await _unitOfWork.Menu.UpdateMenu(_mapper.Map<Menu>(MenuDTO)));
        }

        
        public async Task<IEnumerable<MenuDTO>> GetAllActive()
        {
            var data = _mapper.Map<List<MenuDTO>>(await _unitOfWork.Menu.GetAllActive());
            foreach (var item in data)
            {
                item.DescActive = item.Active == Constants.Active ? Constants.DescActive : item.Active == Constants.Inactive ? Constants.DescInactive : string.Empty;
            }

            return data;
        }

        public async Task<MenuDTO> GetById(string id)
        {
            var data = _mapper.Map<MenuDTO>(await _unitOfWork.Menu.GetById(id));
            if (data != null) { data.DescActive = data.Active == Constants.Active ? Constants.DescActive : data.Active == Constants.Inactive ? Constants.DescInactive : string.Empty; }
            return data == null ? new MenuDTO() : data;
        }

        public async Task<MenuDTO> AddHandler(MenuDTO MenuDTO)
        {
            return await _unitOfWork.Menu.Add(_mapper.Map<Menu>(MenuDTO)) == 0 ? new MenuDTO() : MenuDTO;
        }

        public async Task<IEnumerable<MenuDTO>> GetAll()
        {
            var data = _mapper.Map<List<MenuDTO>>(await _unitOfWork.Menu.GetAll());

            foreach (var item in data) 
            {
                item.DescActive = item.Active == Constants.Active ? Constants.DescActive : item.Active == Constants.Inactive ? Constants.DescInactive : string.Empty;
            }

            return data;
        }
        
    }
}
